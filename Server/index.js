const express = require('express');
const app = express();
const mongoose = require('mongoose');
const helmet = require('helmet')
const dotenv = require('dotenv')
const morgan = require('morgan')
const cors = require('cors');
const authRouter = require('./routers/auth')
const airRouter = require('./routers/airConditioning')
const homeRouter = require("./routers/home")
const livingRoomRouter = require('./routers/livingRoom')
const bedRoomRouter = require('./routers/bedRoom')
const bathRoomRouter = require('./routers/bathRoom')
const kitchenRouter = require('./routers/Kitchen')
const gardenRouter = require('./routers/Garden')
const DatabaseConnect = require('./config/database');


dotenv.config();
DatabaseConnect();
app.use(cors());
app.use(express.json());
app.use(helmet());
app.use(morgan('common'));


app.use("/api/garden",gardenRouter)
app.use("/api/kitchen",kitchenRouter)
app.use("/api/bathroom",bathRoomRouter)
app.use("/api/bedroom",bedRoomRouter)
app.use("/api/livingroom",livingRoomRouter)
app.use("/api/home",homeRouter)
app.use("/api/auth",authRouter)
app.use("/api/air",airRouter)

app.get("/",(req,res)=>{
    res.send("API")
})

app.listen(process.env.PORT,()=>{
    console.log("Server is running")
})

