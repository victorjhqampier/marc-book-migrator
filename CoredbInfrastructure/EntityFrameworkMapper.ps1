#---------------------------------------------------
#------------ Set Variables ------------------------
$DB_HOST = "10.4.12.16"
$DB_NAME = "MiBaseDeDatos"
$DB_TABLES = "--table empresas --table clientes"
#---------------------------------------------------
#---------------------------------------------------
Write-Host "> DB Server: ",$DB_HOST
Write-Host "> DB Name: ",$DB_NAME
$DB_USER = Read-Host -Prompt "> DB User"
$DB_PASSWD = Read-Host -AsSecureString -Prompt "> Passwd for '$DB_USER'"
$plainPassword = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto([System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($DB_PASSWD))
$connectionString = @"
Server=$DB_HOST; Database=$DB_NAME; User=$DB_USER; Password=$plainPassword;TrustServerCertificate=True
"@
$command = "dotnet ef dbcontext scaffold '$connectionString' Microsoft.EntityFrameworkCore.SqlServer -o Collections/Tables -c 'EntityFrameworkContext' $DB_TABLES -f"
Write-Host "> Mapeando Base de Datos (Code First)..."
Invoke-Expression $command
Write-Host "> Ejecución terminado..."