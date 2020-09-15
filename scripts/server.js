const express = require('express')
const compression = require('compression');
const env = require('process').env
const app = express();
const port = 9001
const url = env.NODE_ENV === 'production'
    ? 'http://localhost:' + port // TODO: prod url
    : 'http://localhost:' + port

const dist = __dirname + '/public/'
app.use(compression());
app.use(express.static(dist));

app.listen(port, function(){
    console.log(`Express Server listening on ${url}`);
});