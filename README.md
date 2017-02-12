# Plugins

The Minary plugins are dynamically loaded library files to extend Minary's capabilities.
Minary only spoofs the target systems within the same LAN and reroutes and sniffs the packets sent to/originating from these systems. 
Evaluating the sniffed data packets is outsourced to these extension plugins.

A ***passive*** plugin only evaluates the data packets it receives from the Minary sniffer module. 
It extracts information like DNS requests/responses, HTTP requests or login credentials a user sent to a server. All this data is displayd in
a separate plugin tab the Minary GUI. 

An ***active*** plugin besides parsing and interpreting data packets also modifies packet content. The packet modifications don't serve to attack
a client/server sytem but rather support the attacks that are performed by **intrusive** plugins. Examples of active plugins are the
firewall plugin or the DNS poisoning plugin.

An ***intrusive*** plugin's purpose is to attack a client/server system. It can weaken a systems security measures, inject malicious code into it or
exploit existing vulnerabilities. Examples of such plugins are SSLStrip or InjectPayload.
