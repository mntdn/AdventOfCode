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

main = do  
        let list = []
        handle <- openFile "data.txt" ReadMode
        contents <- getlines handle
        print (sum [
            calcTotal [
                read a :: Int 
                | a <- wordsWhen (=='x') x
                ] 
            | x <- contents
            ])
        hClose handle   

calcTotal :: [Int] -> Int
calcTotal x = (x!!0)*(x!!1)*(x!!2) + (((sort x)!!0 * 2) + ((sort x)!!1 * 2))