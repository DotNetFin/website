# Provisioning with Ansible
1. check if the server is reachable
`ansible cluster  -m ping -i ansible/inventory/hosts.ini `. 
* If it doesn't work, check inventory file for correct ip addresses and user name: `ansible-inventory -i ansible/inventory/hosts.ini --list`
2. run hello-world playbook: `ansible-playbook -i ansible/inventory/hosts.ini hello-world.yaml`
3. shutdown machines: `ansible-playbook -i ansible/inventory/hosts.ini shutdown.yaml` (note: it will print error messages, but it will work)