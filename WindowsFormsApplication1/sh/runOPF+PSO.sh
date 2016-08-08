#Parameters
#$1: 0 for .txt | 1 for .dat
#$2: LibOPF Path 
#$3: Database Path
#$4: Output Path
#$5: input database
#$6: % for training set
#$7: % for testing set
#$8: LibOPT Path
#$9: LibDEV Path
#$10: LibDEEP Path

extension=$1
OPFPath=$2
dataPath=$3
outPath=$4
database=$5
train=$6
test=$7
OPTPath=$8
DEVPath=$9
DEEPPath=$10
outputtxt=$(echo $database | cut -f 1 -d '.')
output=$(echo $database | cut -f 1 -d '.')


if [ $extension = 0 ]; then
    output=$output".dat"
    $OPFPath"/tools/"txt2opf $dataPath"/"$database $outPath"/"$output
    database=$outPath"/"$output
    outputtxt=$outputtxt".txt"
    $OPFPath"/tools/"opf2txt $database $outPath"/"$outputtxt
else
    if [ $extension = 1 ]; then
        output=$output".txt"
        $OPFPath"/tools/"opf2txt $dataPath"/"$database $outPath"/"$output
	database=$dataPath"/"$database
    fi
fi

$OPFPath"/bin/"opf_split $database $train 0 $test 0
$DEVPath"/examples/bin/"FeatureSelection  $outPath"/training.dat"  $outPath"/testing.dat" $outPath"/pso_infos.txt"
$OPFPath"/bin/"opf_train training.dat
$OPFPath"/bin/"opf_classify testing.dat
$OPFPath"/bin/"opf_accuracy testing.dat
$OPFPath"/bin/"opf_train training.pso.dat
$OPFPath"/bin/"opf_classify testing.pso.dat
$OPFPath"/bin/"opf_accuracy testing.pso.dat
