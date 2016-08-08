#Parameters
#$1: 0 for .txt | 1 for .dat
#$2: LibOPF binPath 
#$3: Database Path
#$4: Output Path
#$5: input database
#$6: % for training set
#$7: % for testing set

extension=$1
binPath=$2
dataPath=$3
outPath=$4
database=$5
train=$6
test=$7
outputtxt=$(echo $database | cut -f 1 -d '.')
output=$(echo $database | cut -f 1 -d '.')


if [ $extension = 0 ]; then
    output=$output".dat"
    $binPath"/tools/"txt2opf $dataPath"/"$database $outPath"/"$output
    database=$outPath"/"$output
    outputtxt=$outputtxt".txt"
    $binPath"/tools/"opf2txt $database $outPath"/"$outputtxt
else
    if [ $extension = 1 ]; then
        output=$output".txt"
        $binPath"/tools/"opf2txt $dataPath"/"$database $outPath"/"$output
	database=$dataPath"/"$database
    fi
fi

$binPath"/bin/"opf_split $database $train 0 $test 0
$binPath"/bin/"opf_train training.dat
$binPath"/bin/"opf_classify testing.dat
$binPath"/bin/"opf_accuracy testing.dat
