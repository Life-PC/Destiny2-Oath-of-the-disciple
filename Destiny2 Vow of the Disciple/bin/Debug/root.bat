netsh http add urlacl url=http://+:80/ user=everyone
netsh advfirewall firewall add rule name="Destiny2 Vow of the Disciple Response" dir=in action=allow
netsh advfirewall firewall set rule name="Destiny2 Vow of the Disciple Response" new program=system profile=private protocol=tcp localport=8080
netsh advfirewall firewall add rule name="Destiny2 Vow of the Disciple HTTP" dir=in action=allow
netsh advfirewall firewall set rule name="Destiny2 Vow of the Disciple HTTP" new program=system profile=public protocol=tcp localport=80
netsh advfirewall firewall add rule name="Destiny2 Vow of the Disciple" dir=in action=allow
netsh advfirewall firewall set rule name="Destiny2 Vow of the Disciple" new program="{AppDir}" profile=public
