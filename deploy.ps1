if ($args -contains "clean-build"){dotnet clean}
dotnet build
if ($args -contains "-clean"){
	echo "clean"
	./copydlls.ps1 -clean
} else {
	./copydlls.ps1
}
