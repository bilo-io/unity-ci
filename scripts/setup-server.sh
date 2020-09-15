#!/bin/bash

clear
echo "Fuzzy.BuildWebGLServer => Setting up ExpressServer for WebGL Build"
# Move files around
rm -rf WebGLBuild
mkdir WebGLBuild
cp -r ./Build/ ./WebGLBuild/public
cp ./scripts/server.js ./WebGLBuild/

cd ./WebGLBuild

echo "_________________________________
"
echo "    ✅ Success"
echo "_________________________________"

# Setup Node Server
echo "Fuzzy.BuildWebGLServer => Setup Node.js Server"
npm init -y
npm install express compression nodemon --save
echo "_________________________________
"
echo "    ✅ Success"
echo "_________________________________"

# Install pm2 (crossplatform process manager for Node.js) globally
echo "Fuzzy.BuildWebGLServer => Install pm2 (cross platform process manager for Node.js)"
# npm install pm2 -g
pm2 -v
echo "_________________________________
"
echo "    ✅ Success"
echo "_________________________________"
