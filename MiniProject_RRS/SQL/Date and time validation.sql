
ALTER TABLE TrainDetails
ADD DepartureTime TIME NOT NULL DEFAULT '09:00'

ALTER TABLE TrainDetails
ADD DepartureTime TIME NULL

-- Update each train's departure time manually
UPDATE TrainDetails SET DepartureTime = '09:00' WHERE TrainNo = 100
UPDATE TrainDetails SET DepartureTime = '15:30' WHERE TrainNo = 101

-- Then make it NOT NULL
ALTER TABLE TrainDetails
ALTER COLUMN DepartureTime TIME NOT NULL

