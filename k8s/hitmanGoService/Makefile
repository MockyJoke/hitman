bin:
	cd app && go build -o ../hitmango

debug: bin
	./hitmango

clean:
	rm hitmango

image_local:
	docker build -t johnsonpan333/hitmango:latest .

image:
	docker buildx build --platform linux/amd64,linux/arm64 --tag johnsonpan333/hitmango:latest .

push_image: image
	docker buildx build  --push --platform linux/amd64,linux/arm64 --tag johnsonpan333/hitmango:latest .

debug_image: image
	docker run -it johnsonpan333/hitmango:latest /bin/sh

podman_image:
	podman buildx build --manifest johnsonpan333/hitmango:latest --platform linux/amd64,linux/arm64 --no-cache .

push_podman_image: podman_image
	podman manifest push --all johnsonpan333/hitmango:latest
