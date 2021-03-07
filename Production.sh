git checkout stash@{0} -- production.yml
git checkout stash@{0} -- Dockerfile
docker-compose -f docker-compose.yml -f production.yml build