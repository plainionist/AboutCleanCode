@echo off
setlocal

start dotnet run

timeout /t 5 > nul

nswag openapi2tsclient /input:http://localhost:5101/swagger/v1/swagger.json /output:webui/src/api/client.ts

taskkill /f /im todo.exe > nul

endlocal
