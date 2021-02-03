# RegistryDemo
A netcore 5.0 web site and api for a trust registry,

An implementation is at https://trustregistry.org

# Updating using gitbhu
- add new code to github
- ssh into running droplet
- navigate to code directiory  ( eg cd /home/app/RegistryDemo/)
- docker-compose down
- git stash
- git pull
- git stash pop
- docker-compose -f docker-compose.yml -f production.yml build
- docker-compose -f docker-compose.yml -f production.yml up


