NAME=tobyheighwaydotcom

.PHONY: release local clean start tail

start: release
	# ensure db exists
	sudo mkdir -p /var/lib/${NAME}
	sudo touch /var/lib/${NAME}/database
	sudo setfacl -m u:www-data:rwx /var/lib/tobyheighwaydotcom/database

	# copy dlls
	sudo cp -a bin/Release/netcoreapp3.1/publish /var/${NAME}/

	# start supervisord
	sudo cp ${NAME}.conf /etc/supervisor/conf.d/${NAME}.conf
	sudo systemctl enable supervisor
	sudo systemctl start supervisor
	sudo service supervisor stop
	sudo service supervisor start

local:
	# sudo for db access
	sudo THDC_PORT=5555 dotnet run

release:
	dotnet publish -c Release

clean:
	git clean -fXd

tail:
	tail -f /var/log/${NAME}.out.log
	#tail -f /var/log/${NAME}.out.log
	#tail -f /var/log/nginx/access.log
	#tail -f /var/log/nginx/error.log