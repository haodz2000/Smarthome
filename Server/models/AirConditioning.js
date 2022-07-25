const mongoose = require('mongoose')

const AirConditionSchema = new mongoose.Schema({
    name:{
        type: String,
        default: `TOSHIBA-259 ${Date()}`
    },
    active:{
        type: Boolean,
        default: false
        // true: bật, false: tắt
    },
    temperature:{
        type: Number,
        default: 27,
    },
    mode:{
        type: Number,
        default: 0
        // 0: auto, 1: heat, 2: cool, 3: dry
    }
})

module.exports = mongoose.model("AirConditioning", AirConditionSchema)