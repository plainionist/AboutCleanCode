@echo off
setlocal

set JAR_PATH=build\openapi-generator-cli.jar
set OPENAPI_FILE=openapi.yaml

if not exist build (
    mkdir build
)

if not exist %JAR_PATH% (
    echo Downloading OpenAPI Generator CLI...
    curl -o %JAR_PATH% https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/7.8.0/openapi-generator-cli-7.8.0.jar
)

java -jar %JAR_PATH% generate -i %OPENAPI_FILE% -g csharp -o build/openapi --global-property models,modelTests=false,modelDocs=false ^
    --additional-properties=targetFramework=net8.0,packageName=ViewModels > build\generate.log 2>&1

if %ERRORLEVEL% NEQ 0 (
    echo OpenAPI Generator failed. Check build\generate.log for details.
    exit /b %ERRORLEVEL%
)

xcopy /Y  build\openapi\src\ViewModels\Model\*.cs .
rmdir /S /Q build\openapi

java -jar %JAR_PATH% generate -i %OPENAPI_FILE% -g typescript-fetch -o WebUI/src/api --additional-properties=supportsES6=true > build\generate.log 2>&1

if %ERRORLEVEL% NEQ 0 (
    echo OpenAPI Generator failed. Check build\generate.log for details.
    exit /b %ERRORLEVEL%
)

endlocal
