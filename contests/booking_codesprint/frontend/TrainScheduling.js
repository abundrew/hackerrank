//
// https://www.hackerrank.com/contests/booking-com-passions-hacked-frontend/challenges/javascript-compiler
//

var data = input.split(/\r?\n/);
var n = parseInt(data[0]);
var msgs = [];
for (var i = 0; i < n; i++) {
    var s = data[i + 1].split(':');
    var msg = s[0].trim();
    var delay = 0;
    var s2 = s[1].split(')');
    for (var j = 0; j < s2.length; j++) {
        var s3 = s2[j].split('(');
        for (var k = 0; k < s3.length; k++) {
            var x = parseInt(s3[k].trim()) || 0;
            delay += x;
        }
    }
    if (delay == 0) {
        console.log(msg + '.');
    } else {
        msgs.push({ msg: msg, delay: delay });
    }
}

msgs.sort(function (a, b) { return a.delay - b.delay });

for (var i = 0; i < msgs.length; i++) {
    console.log(msgs[i].msg + '.');
}
