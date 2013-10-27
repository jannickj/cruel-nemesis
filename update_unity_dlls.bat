C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe XmasEngine\XmasProject\XmasProject.sln /p:Configuration=Debug /t:rebuild
copy "XmasEngine\XmasProject\XmasEngineExtensions\bin\Debug\*.dll" UnityTestGame\Assets\dlls
copy "XmasEngine\XmasProject\XmasEngineExtensions\bin\Debug\*.xml" UnityTestGame\Assets\dlls