netsh http add urlacl url=http://+:80/ user=everyone
netsh advfirewall firewall add rule name="Destiny2 Vow of the Disciple" dir=in action=allow
netsh advfirewall firewall set rule name="Destiny2 Vow of the Disciple" new program=system profile=private protocol=tcp localport=80