const router = require('express').Router();
const Garden = require('../models/Garden');
const Kitchen = require('../models/Kitchen');


router.post("/create",async(req,res)=>{
    try {
        const newRoom = new Garden({
            homeId: req.body.homeId,
        })
        const room = await newRoom.save();
        res.status(200).json(room)
    } catch (error) {
        res.status(500).json(error)
    }
})

router.post("/update",async(req,res)=>{
    try {
        const room = await Garden.findOne({
            homeId : req.body.homeId
        })
        !room && res.status(403).json("Not fond garden")
        room.temperature = req.body.temperature
        room.humidity = req.body.humidity
        await room.save();
        res.status(200).json(room)
    } catch (error) {
        res.status(500).json(error)
    }
})

router.get("/find/:homeId",async(req,res)=>{
    try {
        const room = await Garden.findOne({
            homeId : req.params.homeId
        })
        !room&& res.status(403).json("Not found room")
        res.status(200).json(room)
    } catch (error) {
        res.status(500).json(error)
    }
})

router.get("/:id/change/:mode",async(req,res)=>{
    try{
        const room = await Garden.findById(req.params.id)
        !room && res.status(403).json("not found home")
        if(req.params.mode === "on"){
            room.waterPump = true
            await room.save();
            res.status(200).json(room)
        }   
        if(req.params.mode === "off"){
            room.waterPump = false
            await room.save();
            res.status(200).json(room)
        }
    }catch(err){
        res.status(500).json(err)
    }
})
module.exports = router