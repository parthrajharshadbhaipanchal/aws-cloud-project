docker build --rm -t jobtracker:latest .
docker run --rm -p 5124:8080 jobtracker