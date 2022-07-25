const router = require('express').Router();
const BathRoom = require('../models/BathRoom')


router.post("/create",async(req,res)=>{
    try {
        const newRoom = new BathRoom({
            homeId: req.body.homeId,
        })
        const room = await newRoom.save();
        res.status(200).json(room)
    } catch (error) {
        res.status(500).json(error)
    }
})

router.get("/find/:homeId",async(req,res)=>{
    try {
        const room = await BathRoom.findOne({
            homeId : req.params.homeId
        })
        !room&& res.status(403).json("Not found room")
        res.status(200).json(room)
    } catch (error) {
        res.status(500).json(error)
    }
})

router.get("/:id/change/:device/:mode",async(req,res)=>{
    try{
        const room = await BathRoom.findById(req.params.id)
        !room && res.status(403).json("not found home")
        if(req.params.mode === "on"){
            if(req.params.device === "lamp"){
                room.lamp = true
                await room.save();
                res.status(200).json(room)
            }
            if(req.params.device === "heater"){
                room.heater = true
                await room.save();
                res.status(200).json(room)
            }
        }
        if(req.params.mode === "off"){
            if(req.params.device === "lamp"){
                room.lamp = false
                await room.save();
                res.status(200).json(room)
            }
            if(req.params.device === "heater"){
                room.heater = false
                await room.save();
                res.status(200).json(room)
            }
        }
    }catch(err){
        res.status(500).json(err)
    }
})
module.exports = router