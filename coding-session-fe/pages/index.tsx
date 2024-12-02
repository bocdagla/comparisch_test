import type {
  InferGetServerSidePropsType,
  GetServerSideProps,
  NextPage,
} from "next";
import Head from "next/head";
import styles from "../styles/Home.module.css";
import { useState } from "react";

type Listing = {
  title: string;
  dealType: "Rent" | "Buy";
  listingType: "Flat" | "House";
  price: number;
  rooms: number;
  zipcode: number;
  city: string;
};

export const getServerSideProps = (async (context) => {
  const data = await fetch(
    "http://coding-session-be:8080/listing",
  );

  const response = (await data.json()) as Listing[];

  return {
    props: {
      listings: response,
    },
  };
}) satisfies GetServerSideProps<{ listings: Listing[] }>;

const Home = ({
  listings,
}: InferGetServerSidePropsType<typeof getServerSideProps>) => {
  const [filter, setFilter] = useState('all');

  // Filtered listings based on the selected filter
  const filteredListings = filter === 'all'
    ? listings
    : listings.filter((listing) => listing.dealType?.toUpperCase() === filter.toUpperCase());

  return (
    <div>
      <Head>
        <title>comparis.ch</title>
        <meta name="description" content="comparis.ch" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main className={styles.main}>
        <h1 className={styles.title}>Welcome to comparis.ch</h1>
        <h2>Here are our rent properties</h2>
        <label>
          <input
            type="radio"
            name="dealType"
            value="all"
            checked={filter === 'all'}
            onChange={() => setFilter('all')}
          />
          All
        </label>
        <label>
          <input
            type="radio"
            name="dealType"
            value="rent"
            checked={filter === 'rent'}
            onChange={() => setFilter('rent')}
          />
          Rent
        </label>
        <label>
          <input
            type="radio"
            name="dealType"
            value="buy"
            checked={filter === 'buy'}
            onChange={() => setFilter('buy')}
          />
          Buy
        </label>
        <div className={styles.grid}>
          {filteredListings.map((listing, index) => (
            <div className={styles.card} key={index}>
              <h2>{listing.dealType} {listing.listingType} {listing.city}</h2>
              <dl className={styles.details}>
                <dt>Price:</dt>
                <dd>{listing.price}</dd>
                <dt>Rooms:</dt>
                <dd>{listing.rooms}</dd>
                <dt>Adress:</dt>
                <dd>
                  {listing.zipcode} {listing.city}
                </dd>
              </dl>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
};

export default Home;
