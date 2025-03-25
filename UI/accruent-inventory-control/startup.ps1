Start-Process -FilePath 'dotnet' -WorkingDirectory '..\..\API\AccruentInventoryControl' -ArgumentList 'run'
Start-Process -FilePath 'npm' -WorkingDirectory '.' -ArgumentList 'run dev'
