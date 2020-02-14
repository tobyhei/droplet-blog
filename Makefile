DROPLET=root@165.22.240.46
NAME=tobyheighwaydotcom
export THDC_PORT=5555

.PHONY: release local deploy clean start tail

local:
	dotnet run

release:
	dotnet publish -c Release

deploy: clean
	scp -r ./* ${DROPLET}:/root/workplace/${NAME}

clean:
	git clean -fXd

start: release
	# copy dlls
	sudo cp -a bin/Release/netcoreapp3.1/publish /var/${NAME}/

	# start supervisord
	sudo cp ${NAME}.conf /etc/supervisor/conf.d/${NAME}.conf
	sudo systemctl enable supervisor
	sudo systemctl start supervisor
	sudo service supervisor stop
	sudo service supervisor start

tail:
	tail -f /var/log/${NAME}.out.log
	#tail -f /var/log/${NAME}.out.log
	#tail -f /var/log/nginx/access.log
	#tail -f /var/log/nginx/error.log