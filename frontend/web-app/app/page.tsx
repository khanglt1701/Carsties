import Image from "next/image";
import Listings from "./auctions/Listings";

export default async function Home() {
  return (
    <div>
      <div>
        <Listings />
      </div>
    </div>
  );
}
