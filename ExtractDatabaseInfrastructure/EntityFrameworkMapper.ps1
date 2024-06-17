#---------------------------------------------------
#------------ Set Variables ------------------------
$DB_HOST = "192.168.3.132"
$DB_NAME = "_munipunobiblio"
$DB_TABLES = "--table axtitle --table axclasifications --table axauthors --table axpublishers --table axserials --table axcopies"
#---------------------------------------------------
#---------------------------------------------------
Write-Host "> DB Server: ",$DB_HOST
Write-Host "> DB Name: ",$DB_NAME
$DB_USER = Read-Host -Prompt "> DB User"
$DB_PASSWD = Read-Host -AsSecureString -Prompt "> Passwd for '$DB_USER'"
$plainPassword = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto([System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($DB_PASSWD))

$connectionString = @"
Server=$DB_HOST; Database=$DB_NAME; User=$DB_USER; Password=$plainPassword
"@

$command = "dotnet ef dbcontext scaffold '$connectionString' Pomelo.EntityFrameworkCore.MySql -o Collections -c 'EntityFrameworkContext' $DB_TABLES -f"
Write-Host "> Mapeando Base de Datos (Code First)..."
Invoke-Expression $command
Write-Host "> Ejecución terminada..."