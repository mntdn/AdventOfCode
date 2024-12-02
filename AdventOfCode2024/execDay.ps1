param([String]$dayFile) 
& ghc ".\$dayFile.hs"
& .\"$dayFile.exe"