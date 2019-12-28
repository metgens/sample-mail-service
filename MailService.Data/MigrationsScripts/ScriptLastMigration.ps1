# Migration script generator
# This PowerShell script is creating single SQL migration script for last Entity framework migration
# Syntax example: .\ScriptLastMigration.ps1

$initPath = Get-Location

Try
{
	Set-Location -Path "..\"
	$migrationFiles = Get-ChildItem "Migrations\" -Filter *Designer.cs | 
						Sort-Object -Property BaseName |
						Select-Object -Last 2 

	if($migrationFiles.Count -eq 0)
	{
		return
	}

	if($migrationFiles.Count -eq 1)
	{
		$migrationFiles = , @{ BaseName = "0" } + $migrationFiles
	}

	For ($i=0; $i -lt $migrationFiles.Count -1; $i++) {

		$lastFile = $migrationFiles[$i].BaseName -replace '.Designer',''
		$currentFile = $migrationFiles[$i+1].BaseName -replace '.Designer',''

		Write-Host "dotnet ef migrations script" $lastFile " " $currentFile "-o MigrationsScripts\" $currentFile ".sql"
			dotnet ef migrations script $lastFile $currentFile -o MigrationsScripts\$currentFile.sql
	}

	Set-Location -Path "MigrationsScripts"
}
Catch
{
    Write-Host $_.Exception.Message
	Break;
}
Finally
{
    Set-Location -Path $initPath
}
