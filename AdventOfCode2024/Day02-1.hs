import System.IO  
import Control.Monad
import Data.List

wordsWhen     :: (Char -> Bool) -> String -> [String]
wordsWhen p s =  case dropWhile p s of
                      "" -> []
                      s' -> w : wordsWhen p s''
                            where (w, s'') = break p s'

getlines :: Handle -> IO [String]
getlines h = hGetContents h >>= return . lines

getNumbers :: [String] -> [Int]
getNumbers [] = []
getNumbers (x:xs) = (read x :: Int):getNumbers xs

main = do  
        let list = []
        handle <- openFile "data.txt" ReadMode
        contents <- getlines handle
        print ([
            [a+1 |a <- getNumbers . wordsWhen (==' ')]
            | x <- contents
            ])
        hClose handle   
