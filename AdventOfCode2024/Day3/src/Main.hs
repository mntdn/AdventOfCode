module Main (main) where
import System.IO  
import Text.Regex.TDFA

wordsWhen     :: (Char -> Bool) -> String -> [String]
wordsWhen p s =  case dropWhile p s of
                      "" -> []
                      s' -> w : wordsWhen p s''
                            where (w, s'') = break p s'

getlines :: Handle -> IO [String]
getlines h = hGetContents h >>= return . lines

get4 :: (a,b,c,d) -> d
get4 (_,_,_,a) = a

getMult :: String -> Int
getMult s = foldl (*) 1 (map (\x -> read x :: Int) (get4 (s =~ "mul\\(([0-9]+),([0-9]+)\\)" :: (String, String, String, [String]))))

test = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
main = do
  let list = []
  handle <- openFile "./data.txt" ReadMode
  contents <- getlines handle
  putStrLn (show (sum [sum (map getMult (getAllTextMatches (x =~ "mul\\([0-9]+,[0-9]+\\)") :: [String])) | x <- contents]))
  -- putStrLn (show (get4 ("mul(2,4)" =~ "mul\\(([0-9]+),([0-9]+)\\)" :: (String, String, String, [String]))))
  hClose handle   
