def "main" [] { }
def "main 1" [file: path] {
	let input = open -r $file
	let lines = $input | lines
	let cmd = $lines.0 | split chars
	let network = $lines 
	| skip 2
	| parse "{key} = ({L}, {R})"
	| each {|x| { k:$x.key v:($x | reject key) }}
	| transpose -r -d

	mut x = 'AAA'
	mut i = -1
	mut j = -1
	let length = $cmd | length
	while ($x != 'ZZZ') {
		if $j >= ($length - 1) { $j = 0 } else { $j = $j + 1 }
		$i = $i + 1
		let v = $network
		| get $x
		| get ($cmd | get $j)
		# print $'i:($i) x:($x)'
		$x = $v
	} 
	$i + 1
}

def "main 2" [file: path] {
	let input = open -r $file
	let lines = $input | lines
	let cmd = $lines.0 | split chars
	let nodes = $lines 
	| skip 2
	| parse "{key} = ({L}, {R})"

	let network = $nodes
	| each {|x| { k:$x.key v:($x | reject key) }}
	| transpose -r -d

	let starts = $nodes
	| get key
	| where {|x| ($x | split chars | last) == 'A' }

	mut x = $starts

	mut i = -1
	mut j = -1
	let length = $cmd | length
	print $'i:($i) x:($x)'
	while ($x | all {|it| ($it | split chars | last) == 'Z' } | not $in) {
		if $j >= ($length - 1) { $j = 0 } else { $j = $j + 1 }
		$i = $i + 1
		# let v = $network
		# | get $x
		# | get ($cmd | get $j)
		let c = $cmd | get $j
		let v = $x
		| each {|it| $network 
			| get $it 
			| get $c
		}
		$x = $v
		# if $i mod 128 == 0 { print $'i:($i) x:($x)' }
	} 
	$i + 1

}
