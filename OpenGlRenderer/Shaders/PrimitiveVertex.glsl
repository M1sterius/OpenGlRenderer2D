﻿# version 440

layout(location = 0) in vec2 position;

uniform mat4 u_MVP;

void main(){
	gl_Position = vec4(position, 0, 1) * u_MVP;
}
