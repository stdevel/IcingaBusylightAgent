# IcingaBusylightAgent
**Icinga Busylight Agent** is - *how unsual* - an Icinga2 notification agent for [Plenom Kuando Busylights](http://www.plenom.com/). It will connect to your Icinga2 instance and check for host and service events. Faulty hosts and services will generate audible and visible events - you can configure colors and ringtones.

Currently, the software is at a very early stage. Some features are missing, see the **issues** tab for bugs and upcoming functions.

# Demonstration
Check out the following video for a demonstration:

[![IcingaBusylightAgent Alpha: Introduction and basic features ](http://img.youtube.com/vi/s72KDg8Tl7M/0.jpg)](http://www.youtube.com/watch?v=s72KDg8Tl7M)

Additional screenshots can be found [on my blog](http://www.stankowic-development.net/?p=7840&lang=en).

# Requirements
To use this tool, you will need:
- .NET Framework 4.x
- Plenom Kuando Busylight (*I tried Omega, but Alpha should work as well*)
- A valid Icinga2 API user

# Configuration
To configure the agent, you need to specify the following settings:
- Icinga2 URL, e.g. **https://myserver.localdomain.loc:5665**
- API username
- API password

A configure dialog is started by right-clicking the trayicon and clicking ``Configure``.

To create an Icinga2 API user, create a configuration file like this on your Icinga2 server:
```
object ApiUser "busylight" {
  password = "giertz"
  permissions = [ "objects/query/Host", "objects/query/Service" ]
}
```

This will create an API user ``busylight`` with password ``giertz`` and allow reading host and service information. Depending on your Icinga2 configuration, you might also need to enable the API module if it is not already enabled:
```
# icinga2 api setup
# icinga2 feature enable api
# service icinga2 restart
```
