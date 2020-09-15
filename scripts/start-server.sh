#!/bin/bash

pm2 list

case "$1" in
    --dev)   IS_DEV=true ;;
    *)       IS_DEV=false;;
esac

echo "IS_DEV=$IS_DEV"
if $IS_DEV; then
  echo "   DEV: starting node"
  node ./WebGLBuild/server.js
else
  echo "   PROD: starting pm2"
  pm2 start ./WebGLBuild/server.js
fi

echo "_________________________________
"
echo "    ✅ Success"
echo "_________________________________"
