/// <summary>
/// NoiseSimplex.cs
/// Author: trent (5/16/16)
/// Modified: 
/// 
/// Based on the implementation by Jordan Peck: https://github.com/Auburns/FastNoise
/// </summary>
using UnityEngine;

/// <summary>
/// NoiseSimplex Class Definition.
/// 
/// Simplex noise class.
/// </summary>
public class NoiseSimplex : Noise
{
	public static readonly float[,] LUT_Simplex2D =
	{
		{ 0.465254262011416f, -0.885177084927198f }, { 0.999814701091156f, -0.0192500255065206f }, { 0.562013227349949f, 0.827128244157878f }, { 0.540531763173497f, -0.841323607775599f }, { -0.97869393624958f, 0.20532457024989f }, { -0.022259068317433f, 0.999752236245381f }, { -0.763453105361241f, -0.645863264100288f }, { -0.877652307360767f, 0.479297848299283f },
		{ -0.429945697678765f, 0.902854748587789f }, { -0.880336026899474f, 0.474350587374832f }, { -0.999734062007074f, 0.0230609033395389f }, { 0.961877124516138f, -0.273481987217745f }, { 0.485111864938318f, 0.874452101887844f }, { -0.307899752943979f, 0.951418804805243f }, { -0.991765219175154f, 0.128069317303008f }, { 0.937814305853532f, -0.347137332674633f },
		{ -0.398552509901734f, -0.917145515635893f }, { 0.701222178891205f, -0.712942813857514f }, { 0.873193936119955f, 0.487372906431349f }, { -0.514304676201594f, -0.857607544298191f }, { 0.0428414754286589f, 0.999081882521696f }, { -0.88038067821201f, 0.474267710719338f }, { 0.14177264550869f, 0.989899245875795f }, { -0.887664663265946f, -0.46049044027966f },
		{ 0.999983713358517f, -0.00570727760945115f }, { -0.623600504016441f, -0.781743187620104f }, { 0.888019464508127f, -0.459805861918592f }, { 0.600087177175954f, -0.79993460969569f }, { -0.560441030370991f, 0.828194331951567f }, { 0.958552181052623f, -0.284917033887514f }, { 0.6683586377581f, 0.743839183785136f }, { 0.709718808827001f, 0.704485068966819f },
		{ 0.695776987881928f, 0.718257880662615f }, { 0.154316110318401f, -0.988021527142096f }, { -0.725364342269412f, -0.688365143629501f }, { -0.999918869545931f, 0.0127379090116007f }, { 0.805508244111792f, 0.592584566680519f }, { -0.996506623245929f, 0.0835137702836865f }, { -0.73969279786535f, -0.67294469667732f }, { 0.695504007775371f, -0.718522216196824f },
		{ -0.999983227160896f, 0.00579183881678425f }, { -0.714924640549655f, 0.699201514825981f }, { -0.835103934916504f, -0.550092190352645f }, { 0.84959050330652f, 0.527442865807639f }, { -0.171610706906682f, 0.985164841676249f }, { 0.3561994988697f, 0.934409929851441f }, { -0.409182557485348f, -0.912452538299801f }, { 0.463529807851185f, 0.886081326534672f },
		{ -0.962925769084928f, -0.269766497608949f }, { 0.744285209959416f, 0.66786190656128f }, { -0.691027420259623f, 0.722828544296177f }, { 0.981612661017864f, 0.190883691627725f }, { 0.928340009081787f, 0.371732198683444f }, { -0.120562843572988f, 0.992705696946278f }, { 0.789437446513116f, -0.61383101749818f }, { 0.999774655712189f, -0.0212282310985648f },
		{ -0.902267580211854f, 0.431176545858708f }, { -0.912180652347305f, -0.4097883081339f }, { -0.770294485489921f, -0.637688329533965f }, { 0.20063205075057f, 0.979666667908846f }, { -0.982785937329741f, -0.184747940142514f }, { 0.857295252061123f, -0.514825068147866f }, { -0.6880675701609f, -0.725646621223357f }, { 0.703885918434739f, -0.710313039320893f },
		{ 0.80736694412922f, -0.590049673779628f }, { 0.501306939228223f, -0.865269526033149f }, { 0.994382489412099f, -0.105846420584718f }, { 0.496494368044017f, -0.868039942917705f }, { 0.077642279257408f, -0.996981281906393f }, { 0.951970808735996f, -0.30618879684687f }, { -0.761381916275718f, -0.648303615267041f }, { 0.925704511866975f, 0.378247480770363f },
		{ 0.307462004306155f, 0.951560358520699f }, { -0.722833511616144f, -0.6910222243059f }, { 0.365487416741135f, -0.930816280585966f }, { -0.452581511359919f, 0.891723037481466f }, { -0.696052038963733f, -0.717991336336609f }, { -0.731589205376628f, -0.681745725748533f }, { -0.584030912612065f, 0.811731416857521f }, { -0.622817071437575f, -0.782367493909303f },
		{ -0.250124556053565f, -0.968213667771224f }, { -0.972374793807189f, -0.233425063710878f }, { -0.970811073944879f, 0.239845489234194f }, { 0.972096169972212f, -0.234582685476477f }, { -0.56398693111627f, -0.825783713529185f }, { -0.697050386134155f, 0.717022146931478f }, { -0.0346220968223854f, -0.999400475490992f }, { -0.218763172462792f, -0.975777984161364f },
		{ -0.526228595935678f, 0.850343145335791f }, { 0.878475471267297f, 0.477787448957902f }, { -0.543258510315468f, 0.839565477476188f }, { -0.424638392098636f, 0.905363040970795f }, { -0.511241626148254f, -0.859437024855975f }, { 0.310536598007565f, -0.950561424263518f }, { 0.647207900842245f, 0.762313539881967f }, { -0.998374197564832f, 0.0569996634795082f },
		{ -0.384499699032222f, 0.923125116895933f }, { 0.793017652527103f, -0.609198656252953f }, { -0.108016962369769f, 0.994149051118799f }, { -0.931126682943364f, 0.364695901143526f }, { -0.801286360381044f, -0.598281011454734f }, { 0.875902620980454f, -0.482487925817394f }, { -0.731473336626206f, 0.681870044660216f }, { -0.315759562725525f, -0.948839237461956f },
		{ -0.0770741115938651f, -0.997025366438596f }, { -0.822418334736048f, 0.568883188967635f }, { -0.998852401248829f, -0.0478944727442276f }, { 0.996841343178929f, -0.0794187416749304f }, { -0.632437179039341f, 0.774611654036241f }, { 0.719989390873874f, 0.693985069745068f }, { 0.99256596306396f, -0.121707883750037f }, { 0.851153933233715f, -0.524916166583558f },
		{ 0.952866133004913f, 0.303391055524489f }, { 0.983070281370671f, 0.183228878416556f }, { 0.751614346163695f, 0.659602815822462f }, { 0.549915829679874f, 0.83522007894177f }, { 0.529779603383605f, 0.8481353499523f }, { -0.738299650910558f, -0.67447285005799f }, { -0.888225309084572f, -0.459408097775406f }, { 0.69138401830353f, 0.722487466489534f },
		{ 0.895582660271765f, -0.4448951546382f }, { -0.823764839572624f, -0.566931644101728f }, { 0.481527438369519f, 0.876431016165727f }, { -0.393245292146023f, 0.9194335974963f }, { 0.511430719572624f, 0.859324513252955f }, { 0.709782009015835f, 0.704421393540433f }, { -0.123429256704187f, -0.992353373848979f }, { -0.796333188996483f, -0.60485820826264f },
	};

	public static readonly float[,] LUT_Simplex3D =
	{
		{ 0.969192790963703f, -0.204588387787502f, -0.137145636191962f }, { 0.0105050866706296f, 0.869235070511719f, -0.494287401565657f }, { -0.00713466534361336f, -0.0561560433951495f, -0.998396512083569f }, { -0.99998707996014f, 0.00085789886042815f, 0.00501038145637507f }, { -0.729044421579981f, -0.0724223391156089f, 0.680624151907743f }, { -0.769477416524206f, 0.638612501748049f, 0.00886442724220208f }, { -0.148489274637499f, -0.733777362959066f, 0.662964340614538f }, { 0.00192647521945516f, -0.688227670545063f, 0.725492220626345f },
		{ 0.307853961011481f, -0.888296843306306f, 0.340814695782903f }, { -0.136873983993337f, 0.99058218544213f, 0.0035279442294635f }, { 0.981422965662359f, 0.0873011939299262f, -0.170843390299155f }, { 0.977985182924792f, 0.0274298762917452f, 0.206863684261352f }, { -4.10904184278805E-08f, -0.984715746607902f, -0.174169165992265f }, { -0.00576047279073815f, -0.00207046110763061f, 0.999981264896512f }, { 0.385796494451913f, -0.382885124111844f, 0.839380751865608f }, { 0.196898025871543f, -0.533803208609833f, 0.822365674068256f },
		{ -0.953574229510659f, -0.197141111727736f, -0.227665480211439f }, { 0.958192898763756f, -0.00565943080050262f, 0.286067019423289f }, { -0.000021414243574488f, 0.308183457620683f, 0.951326944846192f }, { -0.00156590253858546f, -0.936777289401244f, -0.349922645753741f }, { 0.010162930999189f, 0.907036729301754f, 0.420928837846829f }, { -0.0468274165634939f, 0.74026530347444f, -0.670682095727837f }, { -0.958782958606567f, 0.0676203469753538f, -0.275975953591197f }, { 0.000771226398681087f, -0.963152702156633f, 0.268954043543163f },
		{ -0.939601847652347f, 0.341933173840782f, -0.0151681414637957f }, { 0.0836248033533964f, 0.431468628956066f, 0.898243683245743f }, { -0.97637082132863f, 0.118948680332271f, 0.180419596234078f }, { 0.103518422502263f, 0.000465187240855595f, -0.994627427634827f }, { -0.141353740004181f, 0.0778654829014707f, -0.98689213532146f }, { 0.0700630230682292f, 0.912644877864809f, -0.402703985212052f }, { 0.991874945122624f, -0.0502217417385342f, 0.116884001876814f }, { -0.707119760481927f, 0.700142261026601f, -0.0989063125414175f },
		{ 0.0976913606831578f, 0.99518571412564f, -0.00786081726752686f }, { 0.865756754070504f, 0.268594575188625f, 0.422282129577546f }, { 0.0124864728918991f, -0.922679395826772f, -0.385365826859963f }, { -0.0174011080646746f, -0.984212730893662f, 0.176132057800279f }, { 0.983085383617433f, 0.183144250425653f, 0.00114544916535278f }, { 0.84177325324929f, -0.000849099388075524f, 0.539830593005191f }, { -0.773202768542597f, 0.634158480426922f, -0.000707404184135718f }, { -0.399463762408861f, 0.520122522942901f, -0.754918050949683f },
		{ -0.0543404007696243f, 0.853240000077495f, -0.51867969221086f }, { 0.0787561128671418f, -0.0445112338899157f, -0.995899706167069f }, { -0.149110911377271f, -0.855893524471146f, -0.495189065788614f }, { 0.128579845943545f, 0.95447163354276f, 0.26916746456314f }, { -6.48712081276198E-06f, -0.417292224145094f, 0.908772358528778f }, { -0.18395289837094f, 0.967255425062472f, 0.174866445804071f }, { -0.0956465003848313f, -0.995413983868259f, -0.00165761384418054f }, { -0.493402767926647f, 0.252656257103973f, 0.83229713705415f },
		{ 0.941743815026262f, -0.00313821941824732f, 0.336316426061328f }, { -0.406002839825049f, -0.660621950516609f, -0.631458892208849f }, { -0.0151930656231186f, 0.000118750826499392f, 0.999884571665756f }, { 0.681786958463344f, -0.719431781259153f, 0.132606392695014f }, { 0.973323591185432f, -0.228922756390528f, 0.0153479134887209f }, { -0.810634856852843f, -0.585551171144037f, 0.000977152504195713f }, { -0.181398855060402f, -0.831693356111209f, 0.524767202465292f }, { -0.733900500867695f, 0.679256981569713f, 8.84021970721802E-05f },
		{ 0.271727663466327f, -0.889119678670303f, -0.368280156821335f }, { 0.298233653375395f, -0.0312311598898426f, -0.953981814630813f }, { -0.752318728102205f, -0.00392703305301813f, 0.658787605953604f }, { 0.00665991896444102f, -0.893539727653096f, 0.448934739784101f }, { -0.17274029679022f, 0.981849574845414f, 0.0783083791219516f }, { 0.740984319707759f, -1.24163939998686E-09f, 0.671522328703395f }, { -0.91345982474963f, 0.00101800775270491f, 0.40692764986984f }, { 0.166118424599226f, -0.966950978532905f, -0.193417874362556f },
		{ -0.371630916510155f, 0.92837704130171f, -0.00255559732836468f }, { 0.114876798169617f, 0.991537470331245f, 0.0604712011738797f }, { 0.42509283827761f, -0.861877415693766f, -0.276520160498508f }, { 0.0914177278602429f, 0.937282941514681f, 0.336368081985282f }, { -0.138897098096673f, 0.574329772420649f, -0.806754552916541f }, { 4.20730659972853E-05f, -0.973522779481066f, 0.228590017413966f }, { -0.19883302527571f, -0.769560727972852f, -0.606829229702725f }, { 0.0747279815185906f, 0.375429090417211f, 0.92383371168553f },
		{ 0.0360592551928224f, 0.999349653582228f, 1.40580077597545E-07f }, { -0.630789398266975f, 0.775912761477526f, 0.00800759703705387f }, { 0.999468273136104f, 0.0122075383077114f, -0.0302348640281232f }, { 0.0234858830298487f, 0.722121551769761f, -0.691367397096465f }, { 0.044647769484317f, -0.998966611124733f, -0.00850214902496602f }, { -0.155624581702642f, 0.0567240593672596f, -0.98618627584183f }, { -0.564856042306962f, 0.825189463979779f, 2.46592673497201E-06f }, { -0.00280893077197093f, -0.849678682544325f, 0.527293320968186f },
		{ 0.85760478967376f, 0.0912484314080345f, -0.506149926893406f }, { -0.983676972673489f, -0.138789081908023f, -0.11453036355067f }, { -0.995791780173427f, 0.0916344946719056f, 0.00136011957614869f }, { 0.0705163298667096f, 0.9883873876068f, -0.134602448870495f }, { 0.151288867759186f, -0.946777269461834f, -0.28412053871999f }, { -0.479182164834798f, 2.28582795501955E-06f, 0.877715473772117f }, { -0.920617168247744f, -0.0874211800038508f, -0.380554288918466f }, { 0.647008224881357f, -0.0116892424293656f, -0.76239341454875f },
		{ 0.00348600295131339f, 0.00264876419739902f, 0.999990415869897f }, { 0.293662611616922f, 0.774229458412962f, -0.560652313170918f }, { -0.0230822002633608f, -0.133701316157593f, -0.99075282996756f }, { 0.0891587773276397f, -0.70303221860981f, -0.705546888606285f }, { -0.716380181025389f, -0.675240051032385f, 0.175642562369752f }, { -0.110229513550145f, -0.0380431704478986f, 0.993177814655951f }, { 0.901899360101853f, -0.423583957873496f, -0.0845823556073773f }, { -0.188144760211676f, 0.981876973408649f, 0.0227850454193412f },
		{ -0.992971049488398f, -0.00105311622549315f, -0.118352802350111f }, { 0.974825103742426f, -0.22297088847484f, 2.51166485428216E-06f }, { 0.917736330268161f, 0.253456586759036f, -0.305810050087285f }, { 0.754361752313554f, 0.655909079393725f, -0.0268631013715986f }, { -0.00493426945610691f, 0.999852337890823f, -0.0164607229243032f }, { 0.172163344055272f, 0.977404359568319f, -0.122639719750783f }, { 0.20654798485596f, 0.00802290795582379f, 0.978403578744413f }, { -0.0189935909416278f, -0.956252765802595f, -0.291924461801387f },
		{ -0.298030174309119f, -0.101625749247146f, -0.949131298762838f }, { -0.525057139557522f, -0.851050276233871f, 0.00533174661105069f }, { 0.00362214069523366f, 0.401488407244411f, -0.915856942401558f }, { 0.7517944143271f, 0.19472711592481f, 0.629989292694866f }, { 0.501284099317453f, -0.0225214021165674f, 0.864989617404853f }, { -0.985033512468648f, 0.1680612553394f, -0.0382673982316817f }, { 0.00137633273570574f, -0.0442369494900767f, -0.999020118920542f }, { -0.724166817672979f, -0.688818418185576f, 0.033340770082758f },
		{ -0.0128146550963728f, -0.0397993929245446f, 0.99912551410601f }, { 0.802033957950016f, 0.595775400910827f, -0.0423462154693413f }, { -0.902335114745471f, -0.411652900762455f, -0.127801525777801f }, { 7.40750655087705E-05f, -0.890177016591854f, 0.455614830360591f }, { -0.0563694832642116f, 0.998393830705426f, -0.00567804243293261f }, { 0.804825783463094f, 0.0873894235068948f, -0.587042202002675f }, { 0.902405167679558f, 0.0887442050097385f, -0.421650778989461f }, { 0.0138828478418606f, -0.26021714739708f, 0.965450310858268f },
		{ -0.0235002961872274f, -0.1428517727679f, -0.989465061079056f }, { 0.543378228613975f, 0.00830368325812741f, 0.839446930730399f }, { -0.956960500295924f, 0.0113747804839107f, 0.289995198653906f }, { -0.00858337504492301f, -0.949962410388146f, -0.312246288244364f }, { -0.731827611403257f, 0.00140870300297461f, -0.681488343806152f }, { 0.844453636307317f, 0.498643416559842f, -0.195583228444764f }, { -0.997648624781726f, 0.000128342870118087f, -0.0685361583343983f }, { 0.00700544496566235f, -0.020878882722484f, 0.999757468587804f },
	};

	public static readonly float[,] LUT_Simplex4D =
	{
		{ 0.0179285344989033f, 0.0677944464204167f, 0.900387877028038f, 0.429399757319696f }, { 0.151459767976519f, 0.41940331430838f, 0.025942750707592f, -0.894699822464143f }, { 0.979529859361997f, 0.130367466451577f, -0.00937681736873056f, -0.153093610595927f }, { 0.0188035720634411f, 0.957989228554963f, -0.269676405489343f, 0.0958003130092474f }, { -0.0012369876892f, -7.87528807201163E-05f, 0.000149907955282427f, -0.999999220593219f }, { -0.0515998001407824f, -0.944373511413844f, 0.324801510463498f, 0.000332213707707317f }, { 0.0625583127110917f, 0.368853467530968f, 0.927379950072298f, 7.21458539681357E-05f }, { -0.0332301888025581f, 0.00487951365898265f, -0.999435388671684f, 0.000921286751161818f },
		{ 0.254162385940239f, -0.00247502010266267f, 0.000438725101563024f, 0.967158292819196f }, { -0.759302539566954f, -0.650633735433763f, -0.0116359665914368f, 2.04111122123566E-06f }, { -4.48759951174869E-08f, 0.358287711147997f, -0.933611212169481f, 0.000143358851029576f }, { -0.834876133401752f, -0.194359576306317f, -0.0344898444893627f, -0.513825503066234f }, { 0.992418907081551f, 4.57210674843702E-06f, -1.83490711870738E-06f, 0.122901232063752f }, { -0.42938540950326f, 0.513195588850885f, -0.743126271370401f, -0.00466931352956133f }, { 0.570845731350832f, 1.65553932296542E-05f, 0.0011349034293071f, 0.821056552692115f }, { 2.31617888342864E-05f, 0.994847097173254f, -0.0961577881592109f, -0.0321392670390731f },
		{ 0.0285107567449309f, -0.0502599346018885f, -0.998329141978559f, 2.92045363529652E-10f }, { 0.693061667730217f, 0.081528197183571f, -0.606560258101509f, 0.380924311483053f }, { -0.471772182045896f, -0.105811692623359f, 0.875348441445287f, 3.33769803143138E-06f }, { 0.362766768879719f, 0.923039277394957f, -0.128057658031582f, -1.49328116922925E-06f }, { 0.062046368142898f, -0.815075843570167f, 0.576022235980786f, -3.29295020122024E-05f }, { -0.921787687217261f, 0.28869133264405f, 0.0732935586853114f, -0.248179024911652f }, { -5.47319853185622E-06f, 0.973767617716296f, 0.225257152279963f, -0.0321844994352065f }, { 0.232218374389583f, -0.08472237319722f, -0.137241903519584f, 0.95919831421558f },
		{ -0.0167570140621457f, 0.358790865544362f, 0.933267510677344f, 0.000266074731502954f }, { 0.0107254303176227f, -0.737658920753904f, 1.52650004111161E-06f, -0.675088351087735f }, { -3.71325911057143E-06f, -0.448149367809768f, 0.893952928186452f, 0.00321034340618395f }, { -0.0163066755437812f, -0.0444639669567192f, -0.998877894427004f, -5.06860474025719E-07f }, { 0.085934337625112f, -8.88863502634554E-05f, 0.996300798791311f, 6.62916977146475E-06f }, { 0.018848679157187f, -0.168704308520019f, 0.583111361978302f, 0.794458761115151f }, { -0.14092365680382f, -0.0262056616259223f, 0.00425806403039333f, -0.989664415416913f }, { 0.000019585584043321f, 0.0708482693257697f, 0.094787939700978f, 0.992973196434421f },
		{ -3.61195284939247E-07f, 0.573786610288797f, -0.00565371182001549f, -0.818985324285984f }, { 0.555482972457819f, -0.831524637330389f, 0.00233255322693632f, 6.33832316941843E-05f }, { 0.800371000342067f, 0.168341324992158f, 8.13736079413595E-08f, -0.575384619286367f }, { -0.151943784159287f, -3.17003100215728E-06f, -0.973809705113584f, 0.169138241305451f }, { -3.28617107642979E-10f, -2.51664008452886E-05f, -0.999999889743206f, 0.000468913880721174f }, { 0.144708070244843f, -0.392246876333683f, -0.14750809340107f, 0.896350001279446f }, { -0.499891693213082f, 0.000469340668710032f, -0.0432191264563471f, 0.865008775611117f }, { -0.74530243187983f, -0.0130445109718722f, 0.0435702095816092f, 0.665173483088833f },
		{ -0.796157099668477f, 0.000124118278989671f, -0.00217167212139707f, -0.605086060889134f }, { 0.947484791701284f, 0.0724121319093247f, 3.87510235633725E-05f, -0.311494865359736f }, { 0.999997268027888f, -0.00183696890994059f, 0.00144542293102575f, 0.000015314536027456f }, { 0.246103794412299f, 0.967730041656212f, 0.05414322404818f, -1.18997457774053E-05f }, { 4.28260722805506E-05f, -0.999999938144512f, -0.00024875421527231f, -0.000244945380014729f }, { 0.345932094721763f, -0.00182445228148537f, -2.01050360940934E-07f, 0.938257777593794f }, { 0.232629530802398f, -0.0123421039259699f, -0.000333195046776704f, -0.972487050222468f }, { -0.0413343034477463f, -0.845558019383741f, -0.532041468453896f, 0.0159683142427982f },
		{ 4.876953380543E-11f, -0.509395810571092f, -0.760866765012863f, -0.401992131852703f }, { 0.01457871782159f, 0.00352273519283274f, 2.46006626805202E-08f, 0.999887519335769f }, { -0.2197566484112f, -0.269663250455668f, 0.936190779005179f, -0.0503544649304871f }, { -0.0865455281910523f, 0.0990053597028644f, -2.66646365439371E-05f, 0.991316200608683f }, { -0.989058271919771f, -0.130356511156493f, -0.0535910970499983f, -0.0435764737348075f }, { 0.900013078389142f, -0.00043287841542218f, -2.83509325586139E-08f, -0.435862674869937f }, { -0.771015568252326f, -4.76900694356318E-05f, -0.0219363831312084f, 0.636438360199416f }, { -0.078643371273305f, 0.996901923608738f, -6.16073674743889E-06f, 0.00133222446022512f },
		{ 5.20470758906282E-05f, 0.997256243992129f, 0.0740268623204818f, 6.90275154123082E-05f }, { -0.115896931950432f, -0.300911764975683f, 0.431409257232567f, -0.842559234497932f }, { -1.05936323695781E-10f, -0.0143409648185513f, -0.999897162703251f, 2.73141203266667E-05f }, { 7.36602805763734E-06f, 0.0490840386576873f, -0.187410880606716f, 0.981054493351418f }, { -0.149217229776082f, 0.096219097342591f, 0.984104045608208f, -0.00391549001869267f }, { 0.0301779956805262f, -2.03984355987402E-05f, -0.999544181133284f, -0.00084742150235282f }, { 0.101389753295397f, 0.00105726742868962f, 0.994846219323528f, 3.15683454673068E-06f }, { -0.0132313717977117f, 0.159525525289995f, 0.593084588401323f, -0.789067302948319f },
		{ -0.999950751279126f, -2.28154321862005E-06f, -3.35621991002035E-07f, 0.00992446527495385f }, { -0.446140024906362f, -0.89496104730281f, -0.000949959838625011f, -0.00170281047488805f }, { -0.25830870420065f, -5.99271603499866E-11f, -0.962897969373127f, -0.0781288289384209f }, { -0.0141578535996954f, -0.590108238154547f, 0.772903767582682f, -0.232790868592566f }, { -0.0408946388040428f, -0.487869969082088f, -2.38283754617856E-06f, -0.871957866974805f }, { 0.993390567418713f, 0.114783068234581f, 0.000166763855068009f, -7.22479963187292E-13f }, { -0.150270957910884f, 0.98861713652712f, 0.00724343322227505f, 0.00152618760893165f }, { -0.0292187624948691f, 0.984282454781429f, -2.54315254401317E-06f, -0.174167485832589f },
		{ 0.605498435491149f, 0.0295366828327539f, -0.261172160527168f, 0.75119127494306f }, { -0.00453574210785455f, 0.0164722766698647f, -0.00759597400463439f, -0.99982518088102f }, { -0.161906996023123f, -0.968938255461586f, 0.00479280806368259f, 0.186873782892868f }, { 0.0718297493332595f, -0.15117444225596f, -0.95710224645937f, 0.236520749495151f }, { 1.55364291766971E-08f, 0.751515349174519f, -0.513260578427611f, -0.414473471512055f }, { -0.0832231266679871f, -0.016036973751982f, -0.150197717417989f, 0.985016432524305f }, { -0.0499060179903397f, 0.113056945464528f, 0.760889993198006f, 0.637003873380466f }, { -0.912217939109183f, -0.000236556733323175f, 0.409698193304452f, -0.00240125204303045f },
		{ -0.0201341577237124f, 0.822200561754789f, -0.0353638581788924f, -0.567741357906546f }, { 0.981833737365851f, -0.189743278357464f, 1.93574210616191E-05f, 1.06610391592544E-05f }, { -3.1713467661518E-06f, -0.999998467923194f, -3.10580174237511E-07f, -0.00175046882587876f }, { 0.999785072721677f, 0.00661572799000603f, -0.000560396757787285f, -0.0196399201003463f }, { 0.947369318875243f, 0.10413625625988f, 0.014573335884325f, 0.302381599419074f }, { 0.000539050555161111f, 0.363169172188698f, -0.0860769990608411f, 0.927738439447755f }, { 0.0218600193860152f, 0.0136508664574171f, -0.957380820600311f, 0.287676481040908f }, { 0.0452753695684423f, -3.62412965753121E-05f, 0.412640274130687f, -0.909768181331025f },
		{ -6.91937018588465E-08f, 0.000924049891480408f, -0.998173949198153f, 0.0603979575313267f }, { 0.0524759516860929f, 0.0348710315338021f, -0.997989141515491f, -0.00692524884613554f }, { -0.000620065691267785f, -0.297909553995998f, -0.954593871695887f, -0.000230818326052339f }, { -6.96674135940915E-05f, -0.867754661790055f, 0.496992798829057f, 3.74443548432658E-07f }, { -0.0482481401493802f, 0.00489619615662811f, -0.995799001490995f, 0.0776691242699415f }, { -0.151673443794422f, -0.974774864011805f, -0.000484445743305936f, -0.163734224432859f }, { -0.049803009786826f, 0.0811460037036778f, -0.0626532599996605f, -0.993483545566061f }, { -0.0609700565049769f, 0.169557563496092f, 0.340322391745786f, 0.922883283274476f },
		{ -0.907810279284789f, -9.09566645872125E-05f, -6.82944907490262E-07f, -0.419381077960479f }, { -0.0974613227620325f, 0.995239313157134f, 5.15261588579082E-11f, -1.05825987794001E-05f }, { -0.0186861500087666f, 0.00438118107716855f, 0.992023504204221f, -0.124583306091132f }, { -0.930838673636572f, -0.0110467691436416f, -0.00188202425659556f, 0.365258525620815f }, { -0.0567494686643164f, -2.42763994429726E-09f, 0.975143948519116f, -0.214181645975711f }, { 0.0304023604642125f, -0.632969000014441f, 0.773579295645911f, 0.00100739609776072f }, { 0.000108520483850958f, 0.0583209179359306f, -0.00851497409325953f, -0.998261565908758f }, { -0.02385578591046f, -0.129946314819517f, 0.0562422070557562f, -0.989637141021354f },
		{ -0.00504915323521923f, -0.0089236566539032f, -0.000176312774898594f, 0.999947420276354f }, { 9.54792136888334E-10f, 0.95741607332861f, -0.000200174641238177f, 0.288711659726688f }, { 0.994168345296552f, 0.107836831627415f, 0.000720385221359334f, 1.00702166459814E-07f }, { -0.442903722280488f, 0.0490533033131452f, -0.286717020869704f, 0.848070407553381f }, { -0.579123085419419f, 0.000412386634095739f, 0.000548850089152674f, -0.81523983013291f }, { -0.99748331906203f, -0.00315622682426633f, -0.0314712268933373f, -0.0634557192304806f }, { -0.999910167988914f, 0.00277348539595252f, -0.0131131747511067f, -9.15373542722021E-05f }, { 0.989849756616008f, -0.102100397263962f, -0.0925741321036343f, -0.0346842654676267f },
		{ -0.0502665295186573f, -0.0019502335925594f, 0.995313667448867f, -0.0825843568030342f }, { -0.775008311628356f, 0.00458914577239442f, 0.631802950284916f, 0.012887538916389f }, { -0.917122758231958f, 3.16141922995542E-09f, 0.00144102092010558f, -0.398602270178826f }, { 0.298077607094876f, 0.0476089996782551f, 0.875582290533107f, 0.377145563149066f }, { 0.263407146338272f, -0.473950966620194f, 0.840230003541957f, -0.000835251815231092f }, { -0.55558964464076f, 0.00104388539977359f, -0.000884120434714965f, -0.831455516189702f }, { 0.857295578944858f, -0.0176694994052872f, -0.51054240707814f, -0.0638633673339165f }, { 0.0864515147611175f, -0.35114135064002f, 0.922138824225689f, 0.137425893927874f },
		{ 0.968780126999011f, 0.203315714499582f, -0.141786558529076f, -0.00493534089860906f }, { -0.0103473943005527f, -0.0446849068409796f, -0.625161129502578f, -0.779146810742919f }, { -0.810284066914716f, -0.582923322911606f, -0.0599165375134594f, 0.00708089275712812f }, { -0.914614581816782f, 0.366353079295109f, 0.00173954895166334f, -0.171063035131932f }, { -0.0017333110902601f, 0.0325427046350465f, -0.999147497892685f, 0.0253425622715693f }, { 0.000847670819166468f, 0.0188099412466175f, 0.999636928304481f, 0.019273793982079f }, { 0.100715508011326f, 0.994914522159042f, -0.000565737742910064f, 0.00107702547448881f }, { -0.414687299306508f, -0.734201866849646f, -0.536852323837964f, -0.0277784970462061f },
	};

	public static readonly int[,] LUT_Indices_Simplex4D =
	{
		{ 0, 1, 2, 3 }, { 0, 1, 3, 2 }, { 0, 0, 0, 0 }, { 0, 2, 3, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 2, 3, 0 },
		{ 0, 2, 1, 3 }, { 0, 0, 0, 0 }, { 0, 3, 1, 2 }, { 0, 3, 2, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 3, 2, 0 },
		{ 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
		{ 1, 2, 0, 3 }, { 0, 0, 0, 0 }, { 1, 3, 0, 2 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 2, 3, 0, 1 }, { 2, 3, 1, 0 },
		{ 1, 0, 2, 3 }, { 1, 0, 3, 2 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 2, 0, 3, 1 }, { 0, 0, 0, 0 }, { 2, 1, 3, 0 },
		{ 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
		{ 2, 0, 1, 3 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 3, 0, 1, 2 }, { 3, 0, 2, 1 }, { 0, 0, 0, 0 }, { 3, 1, 2, 0 },
		{ 2, 1, 0, 3 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 3, 1, 0, 2 }, { 0, 0, 0, 0 }, { 3, 2, 0, 1 }, { 3, 2, 1, 0 }
	};

	/// <summary>
	/// NoiseBasic Constructor.
	/// </summary>
	public NoiseSimplex( )
	{
		noiseType = NoiseType.Simplex;
	}

	/// <summary>
	/// Basic 2D dot product.
	/// </summary>
	/// <param name="g">Gradient look-up.</param>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <returns>Dot product</returns>
	static float Dot( float g0, float g1, float x, float y)
	{
		return( g0*x + g1*y );
	}

	const float F2 = 1.0f/2.0f;
	const float G2 = 1.0f/4.0f;

	/// <summary>
	/// Get a 2D simplex noise value.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <returns>Result of 2D simplex noise.</returns>
	float GetSimplex( float x, float y )
	{
		float t = ( x + y )*F2;
		int i = FastFloor( x + t );
		int j = FastFloor( y + t );

		t = ( i + j )*G2;
		float X0 = i - t;
		float Y0 = j - t;

		float x0 = x - X0;
		float y0 = y - Y0;

		int i1 = 0;
		int j1 = 0;
		if( x0 > y0 )
		{
			i1 = 1; 
			j1 = 0;
		}
		else
		{
			i1 = 0; 
			j1 = 1;
		}

		float x1 = x0 - ( float )i1 + G2;
		float y1 = y0 - ( float )j1 + G2;
		float x2 = x0 - 1.0f + 2.0f*G2;
		float y2 = y0 - 1.0f + 2.0f*G2;

		float n0, n1, n2;

		t = 0.6f - x0*x0 - y0*y0;
		if( t < 0 ) n0 = 0;
		else
		{
			t *= t;
			n0 = t * t * Dot( LUT_Simplex2D[GetLUTIndex( i, j ), 0], LUT_Simplex2D[GetLUTIndex( i, j ), 1], x0, y0 );
		}

		t = 0.6f - x1*x1 - y1*y1;
		if( t < 0 ) n1 = 0;
		else
		{
			t *= t;
			n1 = t*t*Dot( LUT_Simplex2D[GetLUTIndex( i + i1, j + j1 ), 0], LUT_Simplex2D[GetLUTIndex( i + i1, j + j1 ), 1], x1, y1 );
		}

		t = 0.6f - x2*x2 - y2*y2;
		if( t < 0 ) n2 = 0;
		else
		{
			t *= t;
			n2 = t*t*Dot( LUT_Simplex2D[GetLUTIndex( i + 1, j + 1 ), 0], LUT_Simplex2D[GetLUTIndex( i + 1, j + 1 ), 1], x2, y2 );
		}

		return( 27.7f*( n0 + n1 + n2 ) );
	}

	/// <summary>
	/// Basic 3D dot product.
	/// </summary>
	/// <param name="g">Gradient look-up.</param>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <returns>Dot product</returns>
	static float Dot( float g0, float g1, float g2, float x, float y, float z )
	{
		return ( g0*x + g1*y + g2*z );
	}

	const float F3 = 1.0f/3.0f;
	const float G3 = 1.0f/6.0f;

	/// <summary>
	/// Get a 3D simplex noise value.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <returns>Result of 3D simplex noise.</returns>
	private float GetSimplex( float x, float y, float z )
	{
		float t = ( x + y + z )*F3;
		int i = FastFloor( x + t );
		int j = FastFloor( y + t );
		int k = FastFloor( z + t );

		t = ( i + j + k )*G3;
		float X0 = i - t;
		float Y0 = j - t;
		float Z0 = k - t;

		float x0 = x - X0;
		float y0 = y - Y0;
		float z0 = z - Z0;

		int i1 = 0;
		int j1 = 0;
		int k1 = 0;
		int i2 = 0;
		int j2 = 0;
		int k2 = 0;
		if( x0 >= y0 )
		{
			if( y0 >= z0 )
			{
				i1 = 1; 
				j1 = 0; 
				k1 = 0; 
				i2 = 1; 
				j2 = 1; 
				k2 = 0;
			}
			else if( x0 >= z0 )
			{
				i1 = 1; 
				j1 = 0; 
				k1 = 0; 
				i2 = 1; 
				j2 = 0; 
				k2 = 1;
			}
			else
			{
				i1 = 0; 
				j1 = 0; 
				k1 = 1; 
				i2 = 1; 
				j2 = 0; 
				k2 = 1;
			}
		}
		else
		{
			if( y0 < z0 )
			{
				i1 = 0; 
				j1 = 0; 
				k1 = 1; 
				i2 = 0; 
				j2 = 1; 
				k2 = 1;
			}
			else if( x0 < z0 )
			{
				i1 = 0; 
				j1 = 1; 
				k1 = 0; 
				i2 = 0; 
				j2 = 1; 
				k2 = 1;
			}
			else
			{
				i1 = 0; 
				j1 = 1; 
				k1 = 0; 
				i2 = 1; 
				j2 = 1; 
				k2 = 0;
			}
		}

		float x1 = x0 - i1 + G3;
		float y1 = y0 - j1 + G3;
		float z1 = z0 - k1 + G3;
		float x2 = x0 - i2 + 2.0f*G3;
		float y2 = y0 - j2 + 2.0f*G3;
		float z2 = z0 - k2 + 2.0f*G3;
		float x3 = x0 - 1.0f + 3.0f*G3;
		float y3 = y0 - 1.0f + 3.0f*G3;
		float z3 = z0 - 1.0f + 3.0f*G3;

		float n0 = 0.0f;
		float n1 = 0.0f;
		float n2 = 0.0f;
		float n3 = 0.0f;

		t = 0.6f - x0*x0 - y0*y0 - z0*z0;
		if( t < 0.0f ) n0 = 0.0f;
		else
		{
			t*= t;
			n0 = t*t*Dot( LUT_Simplex3D[GetLUTIndex( i, j, k ), 0], LUT_Simplex3D[GetLUTIndex( i, j, k ), 1], LUT_Simplex3D[GetLUTIndex( i, j, k ), 2], x0, y0, z0 );
		}

		t = 0.6f - x1*x1 - y1*y1 - z1*z1;
		if( t < 0.0f ) n1 = 0.0f;
		else
		{
			t*= t;
			n1 = t*t*Dot( LUT_Simplex3D[GetLUTIndex( i + i1, j + j1, k + k1 ), 0], LUT_Simplex3D[GetLUTIndex( i + i1, j + j1, k + k1 ), 1], LUT_Simplex3D[GetLUTIndex( i + i1, j + j1, k + k1 ), 2], x1, y1, z1 );
		}

		t = 0.6f - x2*x2 - y2*y2 - z2*z2;
		if( t < 0.0f ) n2 = 0.0f;
		else
		{
			t*= t;
			n2 = t*t*Dot( LUT_Simplex3D[GetLUTIndex( i + i2, j + j2, k + k2 ), 0], LUT_Simplex3D[GetLUTIndex( i + i2, j + j2, k + k2 ), 1], LUT_Simplex3D[GetLUTIndex( i + i2, j + j2, k + k2 ), 2], x2, y2, z2 );
		}

		t = 0.6f - x3*x3 - y3*y3 - z3*z3;
		if( t < 0.0f ) n3 = 0.0f;
		else
		{
			t*= t;
			n3 = t*t*Dot( LUT_Simplex3D[GetLUTIndex( i + 1, j + 1, k + 1 ), 0], LUT_Simplex3D[GetLUTIndex( i + 1, j + 1, k + 1 ), 1], LUT_Simplex3D[GetLUTIndex( i + 1, j + 1, k + 1 ), 2], x3, y3, z3 );
		}

		return( 40.0f*( n0 + n1 + n2 + n3 ) );
	}

	/// <summary>
	/// 2D noise gradient type method.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <returns>Noise value.</returns>
	public override float GetNoise( float x, float y )
	{
		x*= frequency;
		y*= frequency;

		float sum = 0.0f;
		float max = 1.0f;
		float amp = 1.0f;
		uint i = 0;

		int seedPrev = seed;

		switch( fractalType )
		{
			case FractalType.FBM:
				sum = GetSimplex( x, y );

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;

					amp*= gain;
					max+= amp;

					++seed;
					sum+= GetSimplex( x, y )*amp;
				}
			break;

			case FractalType.Billow:
				sum = FastAbs( GetSimplex( x, y ) )*2.0f - 1.0f;

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;
					amp*= gain;
					max+= amp;

					++seed;
					sum+= ( FastAbs( GetSimplex( x, y ) )*2.0f - 1.0f )*amp;
				}
			break;

			case FractalType.RigidMulti:
				sum = 1.0f - FastAbs( GetSimplex( x, y ) );

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;

					amp*= gain;

					++seed;
					sum -= ( 1.0f - FastAbs( GetSimplex( x, y ) ) )*amp;
				}
			break;
		}

		seed = seedPrev;
		return( sum/max );
	}

	/// <summary>
	/// 3D noise gradient type method.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <returns>Noise value.</returns>
	public override float GetNoise( float x, float y, float z )
	{
		x*= frequency;
		y*= frequency;
		z*= frequency;

		x*= frequency;
		y*= frequency;

		float sum = 0.0f;
		float max = 1.0f;
		float amp = 1.0f;
		uint i = 0;

		int seedPrev = seed;

		switch( fractalType )
		{
			case FractalType.FBM:
				sum = GetSimplex( x, y, z );

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;
					z*= lacunarity;

					amp*= gain;
					max+= amp;

					++seed;
					sum+= GetSimplex( x, y, z )*amp;
				}
			break;

			case FractalType.Billow:
				sum = FastAbs( GetSimplex( x, y ) )*2.0f - 1.0f;

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;
					z*= lacunarity;

					amp*= gain;
					max+= amp;

					++seed;
					sum+= ( FastAbs( GetSimplex( x, y, z ) )*2.0f - 1.0f )*amp;
				}
			break;

			case FractalType.RigidMulti:
				sum = 1.0f - FastAbs( GetSimplex( x, y, z ) );

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;
					z*= lacunarity;

					amp*= gain;

					++seed;
					sum -= ( 1.0f - FastAbs( GetSimplex( x, y, z ) ) )*amp;
				}
			break;
		}

		seed = seedPrev;
		return( sum/max );
	}

	static float Dot( float g0, float g1, float g2, float g3, float x, float y, float z, float w)
	{
		return( g0*x + g1*y + g2*z + g3*w );
	}

	static readonly float F4 = ( Mathf.Sqrt( 5.0f ) - 1.0f )/4.0f;
	static readonly float G4 = ( 5.0f - Mathf.Sqrt( 5.0f ) )/20.0f;

	/// <summary>
	/// 4D noise gradient type method (not implemented, just returns 3D noise value).
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <param name="w">w</param>
	/// <returns>Noise value.</returns>
	public override float GetNoise( float x, float y, float z, float w )
	{
		float n0, n1, n2, n3, n4;
		float t = ( x + y + z + w ) * F4;
		int i = FastFloor( x + t );
		int j = FastFloor( y + t );
		int k = FastFloor( z + t );
		int l = FastFloor( w + t );
		t = ( i + j + k + l ) * G4;
		float X0 = i - t;
		float Y0 = j - t;
		float Z0 = k - t;
		float W0 = l - t;
		float x0 = x - X0;
		float y0 = y - Y0;
		float z0 = z - Z0;
		float w0 = w - W0;

		int c = ( x0 > y0 ) ? 32 : 0;
		c += ( x0 > z0 ) ? 16 : 0;
		c += ( y0 > z0 ) ? 8 : 0;
		c += ( x0 > w0 ) ? 4 : 0;
		c += ( y0 > w0 ) ? 2 : 0;
		c += ( z0 > w0 ) ? 1 : 0;
		int i1 = 0;
		int j1 = 0;
		int k1 = 0;
		int l1 = 0;
		int i2 = 0;
		int j2 = 0;
		int k2 = 0;
		int l2 = 0;
		int i3 = 0;
		int j3 = 0;
		int k3 = 0;
		int l3 = 0;

		i1 = LUT_Indices_Simplex4D[c, 0] >= 3 ? 1 : 0;
		j1 = LUT_Indices_Simplex4D[c, 1] >= 3 ? 1 : 0;
		k1 = LUT_Indices_Simplex4D[c, 2] >= 3 ? 1 : 0;
		l1 = LUT_Indices_Simplex4D[c, 3] >= 3 ? 1 : 0;
		i2 = LUT_Indices_Simplex4D[c, 0] >= 2 ? 1 : 0;
		j2 = LUT_Indices_Simplex4D[c, 1] >= 2 ? 1 : 0;
		k2 = LUT_Indices_Simplex4D[c, 2] >= 2 ? 1 : 0;
		l2 = LUT_Indices_Simplex4D[c, 3] >= 2 ? 1 : 0;
		i3 = LUT_Indices_Simplex4D[c, 0] >= 1 ? 1 : 0;
		j3 = LUT_Indices_Simplex4D[c, 1] >= 1 ? 1 : 0;
		k3 = LUT_Indices_Simplex4D[c, 2] >= 1 ? 1 : 0;
		l3 = LUT_Indices_Simplex4D[c, 3] >= 1 ? 1 : 0;

		float x1 = x0 - i1 + G4;
		float y1 = y0 - j1 + G4;
		float z1 = z0 - k1 + G4;
		float w1 = w0 - l1 + G4;
		float x2 = x0 - i2 + 2.0f*G4;
		float y2 = y0 - j2 + 2.0f*G4;
		float z2 = z0 - k2 + 2.0f*G4;
		float w2 = w0 - l2 + 2.0f*G4;
		float x3 = x0 - i3 + 3.0f*G4;
		float y3 = y0 - j3 + 3.0f*G4;
		float z3 = z0 - k3 + 3.0f*G4;
		float w3 = w0 - l3 + 3.0f*G4;
		float x4 = x0 - 1.0f + 4.0f*G4;
		float y4 = y0 - 1.0f + 4.0f*G4;
		float z4 = z0 - 1.0f + 4.0f*G4;
		float w4 = w0 - 1.0f + 4.0f*G4;

		t = 0.6f - x0*x0 - y0*y0 - z0*z0 - w0*w0;
		if( t < 0.0f ) n0 = 0.0f;
		else
		{
			t *= t;
			n0 = t * t * Dot( LUT_Simplex4D[GetLUTIndex( i, j, k, l ), 0], LUT_Simplex4D[GetLUTIndex( i, j, k, l ), 1], LUT_Simplex4D[GetLUTIndex( i, j, k, l ), 2], LUT_Simplex4D[GetLUTIndex( i, j, k, l ), 3], x0, y0, z0, w0 );
		}
		t = 0.6f - x1*x1 - y1*y1 - z1*z1 - w1*w1;
		if( t < 0.0f ) n1 = 0.0f;
		else
		{
			t *= t;
			n1 = t * t * Dot( LUT_Simplex4D[GetLUTIndex( i + i1, j+j1, k+k1, l+l1 ), 0], LUT_Simplex4D[GetLUTIndex( i + i1, j+j1, k+k1, l+l1 ), 1], LUT_Simplex4D[GetLUTIndex( i + i1, j+j1, k+k1, l+l1 ), 2], LUT_Simplex4D[GetLUTIndex( i + i1, j+j1, k+k1, l+l1 ), 3], x1, y1, z1, w1 );
		}
		t = 0.6f - x2*x2 - y2*y2 - z2*z2 - w2*w2;
		if( t < 0.0f ) n2 = 0.0f;
		else
		{
			t *= t;
			n2 = t * t * Dot( LUT_Simplex4D[GetLUTIndex( i + i2, j + j2, k + k2, l + l2 ), 0], LUT_Simplex4D[GetLUTIndex( i + i2, j + j2, k + k2, l + l2 ), 1], LUT_Simplex4D[GetLUTIndex( i + i2, j + j2, k + k2, l + l2 ), 2], LUT_Simplex4D[GetLUTIndex( i + i2, j + j2, k + k2, l + l2 ), 3], x2, y2, z2, w2 );
		}
		t = 0.6f - x3*x3 - y3*y3 - z3*z3 - w3*w3;
		if( t < 0.0f ) n3 = 0.0f;
		else
		{
			t *= t;
			n3 = t * t * Dot( LUT_Simplex4D[GetLUTIndex( i + i3, j + j3, k + k3, l + l3 ), 0], LUT_Simplex4D[GetLUTIndex( i + i3, j + j3, k + k3, l + l3 ), 1], LUT_Simplex4D[GetLUTIndex( i + i3, j + j3, k + k3, l + l3 ), 2], LUT_Simplex4D[GetLUTIndex( i + i3, j + j3, k + k3, l + l3 ), 3], x3, y3, z3, w3 );
		}
		t = 0.6f - x4*x4 - y4*y4 - z4*z4 - w4*w4;
		if( t < 0.0f ) n4 = 0.0f;
		else
		{
			t *= t;
			n4 = t * t * Dot( LUT_Simplex4D[GetLUTIndex( i + 1, j + 1, k + 1, l + 1 ), 0], LUT_Simplex4D[GetLUTIndex( i + 1, j + 1, k + 1, l + 1 ), 1], LUT_Simplex4D[GetLUTIndex( i + 1, j + 1, k + 1, l + 1 ), 2], LUT_Simplex4D[GetLUTIndex( i + 1, j + 1, k + 1, l + 1 ), 3], x4, y4, z4, w4 );
		}

		return( 44.5f*( n0 + n1 + n2 + n3 + n4 ) );
	}
}
