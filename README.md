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
- Icinga2 URL, e.g. **https://myhost.localdomain.loc:5665/**
- API username
- API password

A configure dialog is started by right-clicking the trayicon and clicking ``Configure``. This dialog also gives you additional possibilities for customization (*e.g. sounds, colors, hostgroup filters,...*).
See the [wiki](https://github.com/stdevel/IcingaBusylightAgent/wiki/configuration-client
