module Main (main) where

import System.IO  
import Data.List
import Data.List.Split (splitOn) 

getlines :: Handle -> IO [String]
getlines h = hGetContents h >>= return . lines

twoArrays :: [String] -> Int -> [String]
twoArrays [] _ = []
twoArrays (x:xs) i = (splitOn "   " x)!!i:(twoArrays xs i)

main :: IO ()
main = do
        handle <- openFile "data.txt" ReadMode
        contents <- getlines handle
        let list1 = twoArrays contents 0
        let list2 = twoArrays contents 1
        putStrLn(show( sum [(read x :: Int) * length (elemIndices x list2) | x <- list1] ))
        hClose handle   
