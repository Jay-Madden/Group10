FROM node:12

# Create app directory
WORKDIR /usr/src/ui

# Install app dependencies
COPY package*.json ./

RUN npm install
# If you are building your code for production
# RUN npm ci --only=production

COPY . .

EXPOSE 3000

CMD ["npm", "run", "dev"]
