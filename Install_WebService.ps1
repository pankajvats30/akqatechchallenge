# The following code will create an IIS site and it associated Application Pool. 
# Please note that you will be required to run PS with elevated permissions. 
# Visit http://ifrahimblog.wordpress.com/2014/02/26/run-powershell-elevated-permissions-import-iis-module/ 

# set-executionpolicy unrestricted

$SiteFolderPath = "C:\inetpub\wwwroot\akqa_services"              # Website Folder
$SiteAppPool = "akqaservices"                  # Application Pool Name
$SiteName = "Akqaservices"                        # IIS Site Name
$SiteHostName = "akqaservices.com"            # Host Header

import-module WebAdministration

if(Test-Path ($SiteFolderPath)) {
             Write-Host "Service Folder already exists" -ForegroundColor Yellow
         }else{
			New-Item $SiteFolderPath -type Directory
			}

  if(Test-Path ("IIS:\AppPools\" + $SiteAppPool)) {
             Write-Host "The App Pool $SiteAppPool already exists" -ForegroundColor Yellow
             
         }else{
		 $appPool = New-WebAppPool -Name $SiteAppPool

		 }
  If(!(Test-Path "IIS:\Sites\$SiteName")){
New-Item IIS:\Sites\$SiteName -physicalPath $SiteFolderPath -bindings @{protocol="http";bindingInformation=":80:"+$SiteHostName}
Set-ItemProperty IIS:\Sites\$SiteName -name applicationPool -value $SiteAppPool
}else{
 Write-Host "$SiteName already exists" -ForegroundColor Yellow

}
If ((Get-Content "$($env:windir)\system32\Drivers\etc\hosts" ) -notcontains "127.0.0.1	$SiteHostName")   
 {ac -Encoding UTF8  "$($env:windir)\system32\Drivers\etc\hosts" "127.0.0.1	$SiteHostName" 
 Write-Host "Added host file entry for $SiteHostName" -ForegroundColor Green
 }else{
  Write-Host "Host file entry is exist for $SiteHostName" -ForegroundColor Yellow
 }

Write-Host "Services Hosted Successfully" -ForegroundColor Green

# Complete