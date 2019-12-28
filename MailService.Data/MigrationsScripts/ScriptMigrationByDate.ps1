# Migration script generator
# This PowerShell script is creating single SQL migration script for last Entity framework migration
# Syntax example: .\ScriptMigrationByDate.ps1 20190201

param(
[string]$firstFileName
)
$initPath = Get-Location

Try
{
	Set-Location -Path "..\"
	$migrationFiles = Get-ChildItem "Migrations\" -Filter *Designer.cs | 
						Sort-Object -Property BaseName |
						Where-Object -Property BaseName -ge $firstFileName

	if($migrationFiles.Count -eq 0)
	{
		Break;
	}

	if($migrationFiles.Count -eq 1 -Or $firstFileName -eq "" )
	{
		$migrationFiles = , @{ BaseName = "0" } + $migrationFiles
	}

	For ($i=0; $i -lt $migrationFiles.Count -1; $i++) {

		$lastFile = $migrationFiles[$i].BaseName -replace '.Designer',''
		$currentFile = $migrationFiles[$i+1].BaseName -replace '.Designer',''

		Write-Host "dotnet ef migrations script" $lastFile " " $currentFile "-o MigrationsScripts\" $currentFile ".sql"
			dotnet ef migrations script $lastFile $currentFile -o MigrationsScripts\$currentFile.sql
	}
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