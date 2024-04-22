export class Region
{ 
  regionID?: number
  regionName: string

  constructor(regionID: number, regionName: string) {
    this.regionID = regionID;
    this.regionName = regionName
  }
}
