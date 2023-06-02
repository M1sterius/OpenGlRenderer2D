# version 440

layout(location = 0) in vec2 position;
layout(location = 1) in vec2 v_TexCoords;

uniform mat4 u_MVP;

out vec2 texCoords;

void main(){
    texCoords = v_TexCoords;
    gl_Position = vec4(position, 0, 1) * u_MVP;
}