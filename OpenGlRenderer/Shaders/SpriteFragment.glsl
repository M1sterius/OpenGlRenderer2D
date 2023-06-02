# version 440

in vec2 texCoords;

uniform sampler2D tex;

out vec4 outColor;

void main(){
    outColor = texture(tex, texCoords);
}