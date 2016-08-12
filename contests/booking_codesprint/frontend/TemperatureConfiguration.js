//
// https://www.hackerrank.com/contests/booking-com-passions-hacked-frontend/challenges/the-temperature-configuration
//

function calcTemp(speed, temp1, temp2, hours) {
    if (temp1 < temp2) return Math.min(temp1 + speed * hours, temp2);
    if (temp1 > temp2) return Math.max(temp1 - speed * hours, temp2);
    return temp1;
}

function parseTime(time) {
    var t2 = time.split(' ');
    var t3 = t2[0].split('-');
    var t4 = t2[1].split(':');
    var dt = new Date(parseInt(t3[0], 10), parseInt(t3[1], 10), parseInt(t3[2], 10), parseInt(t4[0], 10), parseInt(t4[1], 10), 0, 0);
    return dt;
}

var data = JSON.parse(input);

var temp = data.initialTemperature;
var endTime = parseTime(data.endTime);

for (var i = 0; i < data.inputs.length; i++) {
    var st = parseTime(data.inputs[i].time);
    var et = endTime;
    if (i + 1 < data.inputs.length) et = parseTime(data.inputs[i + 1].time);
    var hours = (et - st) / (1000 * 3600);
    temp = calcTemp(data.speed, temp, data.inputs[i].temperature, hours);
}

console.log(temp);
