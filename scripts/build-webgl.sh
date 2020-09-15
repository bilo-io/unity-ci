# #/bin/bash
# projectPath=`pwd`
# xvfb-run --auto-servernum --server-args='-screen 0 1024x768x24:32' /opt/Unity/Editor/Unity -batchmode -nographics -logfile stdout.log -force-opengl -quit -projectPath ${projectPath} -buildWebGLPlayer "Builds/web-gl-build"

#/bin/bash

# /Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -executeMethod UnityWebGLBuild.build
/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -buildTarget WebGL