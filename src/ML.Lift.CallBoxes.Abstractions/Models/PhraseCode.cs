namespace ML.Lift.CallBoxes.Abstractions.Models
{
    public enum PhraseCode
    {
        InvalidValidator,

        InvalidDateTimeGenerator,

        InvalidCallBoxFactories,

        MultipleCallBoxFactoriesForSameCallBoxType,

        InvalidMessageFactory,

        InvalidPublisher,

        InvalidIdGenerator,

        InvalidAdminRepository,

        InvalidCallBoxType,

        ValidationCode_BadCreateCallBoxRequest,

        ValidationCode_BadId,

        ValidationCode_BadIds,

        ValidationCode_BadUpdateCallBoxRequest,

        ValidationCode_BadPartialUpdateCallBoxRequest,

        ValidationCode_BadOffset,

        ValidationCode_BadLimit,

        ValidationCode_Valid,

        Missing,

        InvalidMongoClient,

        InvalidMongoOptionAccessor,

        InvalidMongoOptions,

        InvalidMongoOptionsDatabase,

        InvalidMongoOptionsCollection,

        Unknown,

        Success
    }
}
