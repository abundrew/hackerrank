//
// https://www.hackerrank.com/contests/booking-com-passions-hacked-frontend/challenges/the-gps-coordination
//

var obj = JSON.parse(input);

var x1 = obj.p1.start[0];
var y1 = obj.p1.start[1];
var p1 = obj.p1.path;
var x2 = obj.p2.start[0];
var y2 = obj.p2.start[1];
var p2 = obj.p2.path;

var k = 0;

while (x1 !== x2 || y1 !== y2) {
    if (p1[k] == 'R') x1++;
    if (p1[k] == 'D') y1--;
    if (p1[k] == 'L') x1--;
    if (p1[k] == 'U') y1++;
    if (p2[k] == 'R') x2++;
    if (p2[k] == 'D') y2--;
    if (p2[k] == 'L') x2--;
    if (p2[k] == 'U') y2++;
    k++;
}

console.log(x1.toString() + ',' + y1.toString() + ' ' + k.toString());
