def main [] {}
def "main init" [day: int] {
  let url = "https://adventofcode.com"
  let page = http get $'($url)/2023/day/($day)'

  let keywords = $page 
  | query web -q "em"

  let code = $page 
  | query web -q "code"

  let sample = $page 
  | query web -q "pre code"

  let main = $page
  | query web -m -q "main"

  let result = $page
  | query web -q "code em"

  let str_day = $day | fill -w 2 -c 0 -a r
  $main | save -f $'($str_day).main.html'
  $sample | to text | save -r -f $'($str_day)_s1_0' # TODO: split in multiple files
  # { 
  #   main: $main
  #   keywords: $keywords
  #   code: $code
  #   sample: $sample
  #   result: $result
  # }
  # | to json
  echo $"created 2 files for day ($str_day)"
}
