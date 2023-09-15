float4x4 World;
float4x4 View;
float4x4 Projection;

texture indexTexture;
texture lookupTexture;
texture lightMapTexture;

sampler lightMapSampler = 
sampler_state
{
    Texture = < lightMapTexture >;
    MipFilter = POINT;
    MinFilter = POINT;
    MagFilter = POINT;
    AddressU = WRAP;
    AddressV = WRAP;
};

sampler indexSampler = 
sampler_state
{
    Texture = < indexTexture >;
    MipFilter = POINT;
    MinFilter = POINT;
    MagFilter = POINT;
    AddressU = WRAP;
    AddressV = WRAP;
};

sampler lookupSampler =
sampler_state
{
    Texture = < lookupTexture >;
    MipFilter = LINEAR;
    MinFilter = LINEAR;
    MagFilter = LINEAR;
    AddressU = CLAMP;
    AddressV = CLAMP;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 TextureCoordinates : TEXCOORD0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TextureCoordinates : TEXCOORD0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
    output.TextureCoordinates = input.TextureCoordinates;

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{


	float4 color = tex2D(indexSampler, input.TextureCoordinates);
	

	
	float4 light = tex2D(lightMapSampler, input.TextureCoordinates);
	
	float size = 512.0f;
	float lookupSize = 512.0f;
	
	float2 texCoords = input.TextureCoordinates;

	
	float xTex = texCoords.x * size;
	float yTex = texCoords.y * size;
/*	
	xTex = fmod(size, xTex);
	yTex = fmod(size, yTex);
	*/

	int xInt = xTex;
	int yInt = yTex;
	
	xTex -= xInt;
	yTex -= yInt;
	

	if(color.a <= 2.0f/256.0f)
	{
		float scale = color.b;
		float2 offset = float2( (color.r), (color.g ) );
		color = tex2D(lookupSampler, offset+ scale * float2(xTex, yTex));
		//color = float4(1, 0, 1, 1);
	}

	return color;
	//return color * (light + color * 0.1f);
	
	
	
    //return float4(1, 0, 0, 1);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_1_1 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
