const router = require('express').Router();
const AirConditioning = require('../models/AirConditioning')


router.post("/create",async(req,res)=>{
    try {
        const newAir = new AirConditioning(req.body)
        const air = await newAir.save()
        res.status(200).json(air)
    } catch (error) {
        res.status(500).json(error)
    }
})

router.get("/find/:id", async(req,res)=>{
    try {
        const air = await AirConditioning.findById(req.params.id)
        !air && res.status(403).json("Not Found Device")
        res.status(200).json(air);
    } catch (error) {
        res.status(500).json(error)
    }
})

router.get("/change/:id/:type",async(req,res)=>{
    const air = await AirConditioning.findById(req.params.id)
    !air && res.status(403).json("Not found post")
    const tempCurrent = air.temperature
    switch(req.params.type){
        case "desc":{
            air.temperature = tempCurrent>16?tempCurrent -1:16;
            await air.save()
            res.status(200).json(air);
            break;
        }
        case "raise":{
            air.temperature = tempCurrent<30?tempCurrent + 1:30;
            await air.save()
            res.status(200).json(air);
            break;
        }
        case "on":{
            air.active = true
            await air.save()
            res.status(200).json(air);
            break;
        }
        case "off":{
            air.active = false
            await air.save()
            res.status(200).json(air);
            break;
        }
        case "cool":{
            air.mode = 2;
            await air.save()
            res.status(200).json(air);
            break;
        }
        case "dry":{
            air.mode = 3;
            await air.save()
            res.status(200).json(air);
            break;
        }
        case "heat":{
            air.mode = 1;
            await air.save()
            res.status(200).json(air);
            break;
        }
        case "auto":{
            air.mode = 0;
            await air.save()
            res.status(200).json(air);
            break;
        }
        default:
            break;
    }
    
})

module.exports = router