(

//TODO: Figure out how to not use full path
~moveVar = Buffer.read(Server.default,
	"C:/Users/Bormaj/Documents/GitHub/Death_Tastes_Funny/SuperCollider/Audio/Step_Example.aiff");

//Sampler
SynthDef(\sampler, {
	arg out = 0, rate = 1, amp = 1, buffer = 0;
	var signal;

	signal =
	PlayBuf.ar(2, buffer,
		BufRateScale.kr(buffer)*rate,
		doneAction:2
	);

	Out.ar(out, signal!2 * amp);

}).add;


//Move OSC
OSCdef.new(
	\move,
	{arg msg;
		[msg].postln;

		Synth(\sampler, [\buffer, ~moveVar]);
	},
	'/move',
	nil, 57120
);
)
