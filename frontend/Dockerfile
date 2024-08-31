# Build stage
FROM node:alpine AS build-env
WORKDIR /Frontend
COPY . .
RUN npm install
RUN npm run build

# Serve stage
FROM node:alpine
WORKDIR /Frontend
COPY --from=build-env /Frontend/build .
RUN npm install -g serve
CMD ["serve", "-s", "."] 
