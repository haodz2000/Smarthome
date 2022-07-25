const dotenv = require('dotenv')
const mongoose = require('mongoose')
dotenv.config()

const Connect = async()=>{
    try{
        await mongoose.connect(process.env.MONGO_URL,
            {useNewUrlParser: true},
            ()=>{
                console.log("Database connect successful");
            }    
            )
    }
    catch(errrr){

    }
}
module.exports = Connect;