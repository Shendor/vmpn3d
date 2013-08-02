sampler2D input : register(s0);
sampler2D oldInput : register(s1);

float progress : register(c0);

float4 main(float2 uv : TEXCOORD) : COLOR 
{ 	
	float4 oldColor = tex2D(oldInput, uv);
	float4 newColor = tex2D(input, uv);
	
	float a = (4.0 * progress - (uv.x + uv.y)) * 0.5;
	
	if (a < 0) a = 0;
	if (a > 1) a = 1;
	
	return oldColor * (1 - a) + newColor * a;
}
