//
// https://www.hackerrank.com/contests/booking-com-passions-hacked-frontend/challenges/the-elevator-superstition
//

var x = 0;
for (var i = 1; i <= input; i++) {
    x++;
    var s = x.toString();
    while (s.indexOf('13') !== -1 || s.indexOf('4') !== -1) {
        x++;
        s = x.toString();
    }
}
console.log(x);
