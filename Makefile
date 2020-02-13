DROPLET=root@165.22.240.46
NAME=tobyheighwaydotcom
BIN=bin/Release/netcoreapp3.1/linux-x64/publish

.PHONY: run deploy clean start-service test-start-service tail

tobyheighwaydotcom:
	dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true
	cp ${BIN}/${NAME} .

run: tobyheighwaydotcom
	sudo ./${NAME}

deploy: clean
	scp -r ./* ${DROPLET}:/root/workplace/tobyheighwaydotcom

clean:
	git clean -fXd

start-service: tobyheighwaydotcom
	# configure and start nginx
	sudo cp nginx.conf /etc/nginx/sites-available/default
	sudo nginx -t 
	sudo nginx -s reload

	# copy binary
	sudo mkdir -p /var/${NAME}
	sudo cp -a ${NAME} /var/${NAME}/${NAME}

	# start supervisord
	sudo cp ${NAME}.conf /etc/supervisor/conf.d/${NAME}.conf
	sudo systemctl enable supervisor
	sudo systemctl start supervisor
	sudo service supervisor stop
	sudo service supervisor start

test-start-service: tobyheighwaydotcom
	/usr/bin/supervisord

tail:
	tail -f /var/log/${NAME}.out.log
	#tail -f /var/log/${NAME}.out.log
	#tail -f /var/log/nginx/access.log
	#tail -f /var/log/nginx/error.log